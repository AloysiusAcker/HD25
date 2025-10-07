<%@ Page Title="" Language="VB" MasterPageFile="~/EvaluacionProcesos/PagPrincipal_EvalProceso.master" AutoEventWireup="false" CodeFile="EvalProcesos_Prevencion_Ingreso.aspx.vb" Inherits="EvaluacionProcesos_EvalProcesos_Prevencion_Ingreso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label5" runat="server" Text="Encuesta de Prevencion para el ingreso a tienda ante el Covid-19" CssClass="Titulos"></asp:Label><br />
    <br />
    <asp:Button ID="BtnReporte" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Exportar" Visible="false"  />
    <asp:UpdatePanel ID="UpdatePanel19" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
            <asp:Label ID="LblError" runat="server" ForeColor="Red"></asp:Label>
            <div id="DivBusqueda" runat="server" visible="true" >
	            <div class="form-group">
                    <asp:Label ID="LblEt1" runat="server" Text="DNI" CssClass="col-lg-2 control-label-2" ></asp:Label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="TxtDNI" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>                       
                                 
	            <div class="form-group">
                    <asp:Label ID="LblEt2" runat="server" Text="Nombres" CssClass="col-lg-2 control-label-2" ></asp:Label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="TxtNombres" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="LblEt3" runat="server" CssClass="col-lg-2 control-label-2" Text="Apellidos"></asp:Label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="TxtApellidos" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div> 
            <div id="DivDatosPersonal" runat="server" visible="false" >

                <div class="form-group">
                    <asp:Label ID="LblEt4" runat="server" Text="Codigo" CssClass="col-lg-2 control-label-2" ></asp:Label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="TxtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="LblEt5" runat="server" CssClass="col-lg-2 control-label-2" Text="Apellidos y Nombres"></asp:Label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="TxtNombresCompleto" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LblEt19" runat="server" CssClass="col-lg-2 control-label-2" Text="DNI"></asp:Label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="TxtDNI2" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LblEt20" runat="server" CssClass="col-lg-2 control-label-2" Text="EDAD"></asp:Label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="TxtEdad" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LblEt18" runat="server" CssClass="col-lg-2 control-label-2" Text="Distrito donde reside"></asp:Label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="TxtDistrito" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="LblEt6" runat="server" CssClass="col-lg-2 control-label-2" Text="Restaurante / Sede"></asp:Label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="TxtSede" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="LblEt7" runat="server" CssClass="col-lg-2 control-label-2" Text="Puesto"></asp:Label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="TxtPuesto" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div> 

            <div id="DivIngreso" runat="server" visible="false" >
                <br />
                <br />
                <asp:Label ID="LblEt8" runat="server" CssClass="subTitulos_left" Text="Responde si tienes algunos de estos sintomas:"></asp:Label>
                <br />
                <br />
                
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" CssClass="col-lg-2 control-label-2" Text="Fecha y Hora"></asp:Label>
                    <div class="col-lg-2">
                        <asp:TextBox ID="TxtFecha" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-lg-2">
                        <asp:TextBox ID="TxtHora" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="LblEt9" runat="server" CssClass="col-lg-2 control-label-2" Text="Temperatura"></asp:Label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="TxtTemperatura" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            
                <div class="form-group">
                    <asp:Label ID="LblEt10" runat="server" CssClass="col-lg-2 control-label-2" Text="TOS"></asp:Label>
                    <div class="col-lg-5">
                        <asp:DropDownList ID="DdlTOS"  CssClass="form-control" runat="server">
                            <asp:ListItem>SI</asp:ListItem>
                            <asp:ListItem Selected="True">NO</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="LblEt11" runat="server" CssClass="col-lg-2 control-label-2" Text="Dolor de Garganta"></asp:Label>
                    <div class="col-lg-5">
                        <asp:DropDownList ID="DdlDolorG"  CssClass="form-control" runat="server">
                            <asp:ListItem>SI</asp:ListItem>
                            <asp:ListItem Selected="True">NO</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                              
                <div class="form-group">
                    <asp:Label ID="LblEt12" runat="server" CssClass="col-lg-2 control-label-2" Text="Estornudos y congestion nasal"></asp:Label>
                    <div class="col-lg-5">
                        <asp:DropDownList ID="DdlEstornudos"  CssClass="form-control" runat="server">
                            <asp:ListItem>SI</asp:ListItem>
                            <asp:ListItem Selected="True">NO</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="LblEt13" runat="server" CssClass="col-lg-2 control-label-2" Text="Dificultad Respiratoria"></asp:Label>
                    <div class="col-lg-5">
                        <asp:DropDownList ID="DdlDifRespirar"  CssClass="form-control" runat="server">
                            <asp:ListItem>SI</asp:ListItem>
                            <asp:ListItem Selected="True">NO</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            
                <div class="form-group">
                    <asp:Label ID="LblEt14" runat="server" CssClass="col-lg-2 control-label-2" Text="Dolor muscular o malestar general"></asp:Label>
                    <div class="col-lg-5">
                        <asp:DropDownList ID="DdlMalestar"  CssClass="form-control" runat="server">
                            <asp:ListItem>SI</asp:ListItem>
                            <asp:ListItem Selected="True">NO</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                        
                <div class="form-group">
                    <asp:Label ID="LblEt15" runat="server" CssClass="col-lg-2 control-label-2" Text="Comentario"></asp:Label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="TxtObs" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                        
                <div class="form-group">
                    <asp:Label ID="LblEt16" runat="server" CssClass="col-lg-2 control-label-2" Text="Temperatura medio turno"></asp:Label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="TxtTempMT" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                        
                <div class="form-group">
                    <asp:Label ID="LblEt17" runat="server" CssClass="col-lg-2 control-label-2" Text="Temperatura Final"></asp:Label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="TxtTempFinal" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            
            </div>

            <div class="form-group">
                <asp:Button ID="BtnListar" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Buscar" />
                <asp:Button ID="BtnGuardar" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Guardar" visible="false" />
                <asp:Button ID="BtnCancelar" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Cancelar" Visible="false"  />
            </div>
                                    
            <div id="DivDetalle" runat="server"  class="row form-group col-md-10" visible="false">
                <asp:GridView id="gvDetalle" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                    <Columns>

                    <asp:BoundField DataField="C1" HeaderText="">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="C2" HeaderText="">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="C3" HeaderText="">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="C4" HeaderText="">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="C5" HeaderText="">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="C6" HeaderText="">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="C7" HeaderText="">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                </asp:GridView>
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

                    <asp:ButtonField ButtonType="Image" CommandName="Detalle" Text="Detalle" ImageUrl="~/Icono/details_opt.png">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    </asp:ButtonField>

                    <asp:BoundField DataField="CODIGO" HeaderText="C&#243;digo">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="NOMBRES" HeaderText="Descripci&#243;n">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="DNI" HeaderText="DNI">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="EDAD" HeaderText="Edad">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="RESTAURANTE_SEDE" HeaderText="Restaurante/Sede">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PUESTO" HeaderText="Puesto">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="RAZON_SOCIAL" HeaderText="Raz&#243;n Social">
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
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

