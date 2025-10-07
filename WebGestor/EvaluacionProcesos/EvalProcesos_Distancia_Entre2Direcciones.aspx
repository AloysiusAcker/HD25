<%@ Page Title="" Language="VB" MasterPageFile="~/EvaluacionProcesos/PagPrincipal_EvalProceso.master" AutoEventWireup="false" CodeFile="EvalProcesos_Distancia_Entre2Direcciones.aspx.vb" Inherits="EvaluacionProcesos_EvalProcesos_Distancia_Entre2Direcciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label5" runat="server" Text="Distancia entre dos Direcciones" CssClass="Titulos"></asp:Label><br />
    <br />
    <asp:Label ID="LblError" runat="server" ForeColor="Red"></asp:Label>
    <asp:UpdatePanel ID="UpdatePanel19" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
	        <div class="form-group">
                <asp:Label ID="LblEt1" runat="server" Text="Tipo de Distancia" CssClass="col-lg-2 control-label-2" ></asp:Label>
                <div class="col-lg-5">
                    <asp:DropDownList ID="DdlTipoDistancia" runat="server"  CssClass="form-control" AutoPostBack="True">
                        <asp:ListItem Value="0">Entre Personal x Oficina</asp:ListItem>
                        <asp:ListItem Value="1" Selected="True">Entre Oficinas x Personal</asp:ListItem>
                    </asp:DropDownList>
                </div>   
            </div>            
            
	        <div class="form-group">
                <asp:Label ID="LblEt2" runat="server" Text="Oficina" CssClass="col-lg-2 control-label-2" ></asp:Label>
                <div class="col-lg-5">
                    <asp:DropDownList ID="DdlOficina" runat="server"  CssClass="form-control" Enabled="False" AutoPostBack="True">
                    </asp:DropDownList>
                </div>   
            </div>

            <div class="form-group">
                <asp:Label ID="LblEt3" runat="server" Text="Personal" CssClass="col-lg-2 control-label-2" ></asp:Label>
                <div class="col-lg-5">
                    <asp:DropDownList ID="DdlPersonal" runat="server"  CssClass="form-control" AutoPostBack="True">
                    </asp:DropDownList>
                </div>  
            </div>
            
            <div class="form-group">
                <asp:Label ID="LblEt5" runat="server" CssClass="col-lg-2 control-label-2" Text="Cargo"></asp:Label>
                <div class="col-lg-5">
                    <asp:DropDownList ID="DdlCargo" runat="server"  CssClass="form-control" Enabled="False" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>
            
            <div class="form-group">
                <asp:Label ID="LblEt6" runat="server" CssClass="col-lg-2 control-label-2" Text="Estado"></asp:Label>
                <div class="col-lg-5">
                    <asp:DropDownList ID="DdlEstado" runat="server"  CssClass="form-control" Enabled="False" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="LblEt4" runat="server" CssClass="col-lg-2 control-label-2" Text="Direccion"></asp:Label>
                <div class="col-lg-5">
                    <asp:TextBox ID="TxtDireccion" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="LblLatitud" runat="server" CssClass="col-lg-2 control-label-2" Text="Latitud"  ></asp:Label>
                <div class="col-lg-5">
                    <asp:TextBox ID="TxtLatitud" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="LblLongitud" runat="server" CssClass="col-lg-2 control-label-2" Text="Longitud" ></asp:Label>
                <div class="col-lg-5">
                    <asp:TextBox ID="TxtLongitud" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                </div>
            </div>
            
            <div class="form-group">
                <asp:Button ID="BtnListar" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Listar" />
            </div>
            
             <div class="form-group">
                <asp:Label ID="LblRegistro" runat="server"  Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon" ></asp:Label>
            </div>

            <div class="row form-group col-md-10">
                <asp:GridView id="Flex" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                    <Columns>
                    <asp:BoundField DataField="CODIGO" HeaderText="C&#243;digo">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="NOMBRES" HeaderText="Descripci&#243;n">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="DIRECCION" HeaderText="Direcci&#243;n">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Latitud" HeaderText="Latitud">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Longitud" HeaderText="Longitud">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Distancia" HeaderText="Distancia en Km">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                    </asp:BoundField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                </asp:GridView>
            </div>


        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="DdlTipoDistancia" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="DdlPersonal" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="DdlOficina" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

