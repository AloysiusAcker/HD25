<%@ Page Title="" Language="VB" MasterPageFile="~/EvaluacionProcesos/PagPrincipal_EvalProceso.master" AutoEventWireup="false" CodeFile="EvalProcesos_Define_Oficinas.aspx.vb" Inherits="EvaluacionProcesos_EvalProcesos_Define_Oficinas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:Label ID="Label5" runat="server" Text="Define Oficina" CssClass="Titulos"></asp:Label><br />

    <asp:UpdatePanel ID="UpdatePanel19" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
            <asp:Label ID="LblError" runat="server" ForeColor="Red"></asp:Label>

            <div id="DivDatosOficina" runat="server" visible="false" >
                
                <div class="form-group">
                    <asp:Label ID="LblEt1" runat="server" Text="Codigo Interno" CssClass="col-lg-2 control-label-2" ></asp:Label>
                    <div class="col-lg-1">
                        <asp:TextBox ID="TxtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="LblEt2" runat="server" CssClass="col-lg-2 control-label-2" Text="Descripcion"></asp:Label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="TxtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LblEt3" runat="server" CssClass="col-lg-2 control-label-2" Text="Tipo"></asp:Label>
                    <div class="col-lg-5">                        
                        <asp:DropDownList ID="DdlTipo"  CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LblEt4" runat="server" CssClass="col-lg-2 control-label-2" Text="Canales"></asp:Label>
                    <div class="col-lg-5">                        
                        <asp:CheckBoxList ID="ChkCanales" runat="server" RepeatDirection="Vertical" CellPadding="10" CellSpacing="10"></asp:CheckBoxList>
                    </div>
                </div>
                
                <div class="form-group">
                    <asp:Label ID="LblEt5" runat="server" CssClass="col-lg-2 control-label-2" Text="Direccion"></asp:Label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="TxtDireccion" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="LblEt6" runat="server" CssClass="col-lg-2 control-label-2" Text="Departamento"></asp:Label>
                    <div class="col-lg-5">                        
                        <asp:DropDownList ID="DdlDpto"  CssClass="form-control" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="LblEt7" runat="server" CssClass="col-lg-2 control-label-2" Text="Provincia"></asp:Label>
                    <div class="col-lg-5">                        
                        <asp:DropDownList ID="DdlProvincia"  CssClass="form-control" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LblEt8" runat="server" CssClass="col-lg-2 control-label-2" Text="Distrito"></asp:Label>
                    <div class="col-lg-5">                        
                        <asp:DropDownList ID="DdlDistrito"  CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="LblEt9" runat="server" CssClass="col-lg-2 control-label-2" Text="Latitud"></asp:Label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="TxtLatitud" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LblEt10" runat="server" CssClass="col-lg-2 control-label-2" Text="Longitud"></asp:Label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="TxtLongitud" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>     
<%--                <div class="form-group">
                <asp:Button ID="btnSearch" runat="server" Text="Buscar coordenadas" />
                </div>    --%>
                <div class="form-group">
                    <asp:Label ID="LblCodigo" runat="server" CssClass="col-lg-2 control-label-2" Text="" Visible="false" ></asp:Label>                    
                </div>
            </div>                      
            <div id="DivHora" runat="server"  class="row form-group col-md-10">
                <asp:GridView ID="FlexHora" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                    <Columns>
                        <asp:BoundField DataField="c0" HeaderText="#">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="c1" HeaderText="Día">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="De">
                            <ItemTemplate>
                                <asp:DropDownList ID="cboD1" runat="server" Font-Names="Arial" Font-Size="8pt" Width="50px">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="A">
                            <ItemTemplate>
                                <asp:DropDownList ID="cboA1" runat="server" Font-Names="Arial" Font-Size="8pt" Width="50px">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="De">
                            <ItemTemplate>
                                <asp:DropDownList ID="cboD2" runat="server" Font-Names="Arial" Font-Size="8pt" Width="50px">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="A">
                            <ItemTemplate>
                                <asp:DropDownList ID="cboA2" runat="server" Font-Names="Arial" Font-Size="8pt" Width="50px">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="c6">
                        <ItemStyle ForeColor="White" Width="0px" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>
            </div>            
            <div class="form-group">
                <asp:Label ID="LblMensaje" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Button ID="BtnListar" runat="server" ControlStyle-CssClass="btn btn-default" Text="Listar" />
                <asp:Button ID="BtnNuevo" runat="server" ControlStyle-CssClass="btn btn-default" Text="Agregar" />
                <asp:Button ID="BtnGuardar" runat="server" ControlStyle-CssClass="btn btn-default" Text="Guardar" visible="false" />
                <asp:Button ID="BtnCancelar" runat="server" ControlStyle-CssClass="btn btn-default" Text="Cancelar" Visible="false"  />
                <asp:Button ID="btnSearch" runat="server" ControlStyle-CssClass="btn btn-default"  Text="Buscar coordenadas"  Visible="false"/>
            </div>           
            <div class="form-group">
                <asp:Label ID="LblRegistro" runat="server"  Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon" ></asp:Label>
            </div>
            <div id="DivFlex" runat="server"  class="row form-group col-md-10">
                <asp:GridView id="Flex" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                    <Columns>
                    <asp:ButtonField ButtonType="Image" CommandName="Editar" Text="Editar" ImageUrl="~/Icono/Editar_opt.png">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:ButtonField>

                    <asp:ButtonField ButtonType="Image" CommandName="Horario" Text="Horario de Atenci&#243;n" ImageUrl="~/Icono/EDITAR_TIEMPO_opt.png">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:ButtonField>
                                        
                    <asp:BoundField DataField="OFICINA_CODIGO" HeaderText="C&#243;digo">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="OFICINA_COD_INTERNO" HeaderText="C&#243;digo Interno">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="OFICINA_NOMBRE" HeaderText="Descripci&#243;n">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="TIPO" HeaderText="Tipo">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="CANAL" HeaderText="Canales">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="DIRECCION" HeaderText="Direcci&#243;n">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="DPTO" HeaderText="Departamento">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PROVINCIA" HeaderText="Provincia">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="DISTRITO" HeaderText="Distrito">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="LATITUD" HeaderText="Latitud">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="LONGITUD" HeaderText="Longitud">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>

                    </Columns>
                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                </asp:GridView>
            </div>


        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="BtnCancelar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnGuardar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>


</asp:Content>

