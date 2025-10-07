<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Permisos.aspx.vb" Inherits="Eventos_Permisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="container">
        <h1 class="Titulos">Permisos del Personal</h1>
        <div class="row espacio">
            <div class="col-lg-2">
                <asp:Button ID="BtnListar" runat="server" text="Listar" CssClass="form-control btn-default" /> 
            </div>
            <div class="col-lg-2">
                <asp:Button ID="BtnNuevo" runat="server" text="Nuevo Permiso" CssClass="form-control btn-default" /> 
            </div>
        </div>        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div id="DivPermiso" runat="server" visible="false" >                    
                    <div class="row espacio">
                        <div class="col-lg-12">
                            <asp:Label id="LblEtiq1" runat="server" CssClass="control-label-2" text="Datos del Permiso" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                        </div>
                    </div>                
                    <div class="row espacio">
                        <div class="col-lg-2">
                            <asp:Label id="LblEtiq2" runat="server" CssClass="control-label-2" text="Código del Permiso" ></asp:Label>
                            <asp:TextBox ID="TxtPerCodigo" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                        </div>
                        <div class="col-lg-4">
                            <asp:Label id="LblEtiq3" runat="server" CssClass="control-label-2" text="Tipo del Permiso" ></asp:Label>
                            <asp:DropDownList ID="DdlTipo" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                    </div>          
                    <div class="row espacio">                        
                        <div class="col-lg-8">
                            <asp:Label id="LblEtiq13" runat="server" CssClass="control-label-2" text="Personal" ></asp:Label>
                            <asp:DropDownList ID="DdlPersonal" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row espacio">
                        <div class="col-md-2">
                            <asp:Label ID="LblEtiq9" runat="server" CssClass="control-label-2"  Text="Fecha Inicia"></asp:Label>
                            <asp:TextBox ID="TxtFechaIni" runat="server"  CssClass="form-control" TextMode="Date" ></asp:TextBox>
                        </div> 
                        <div class="col-md-2">
                            <asp:Label ID="LblEtiq10" runat="server" CssClass="control-label-2"  Text="Fecha Termina"></asp:Label>
                            <asp:TextBox ID="TxtFechaFin" runat="server"  CssClass="form-control" TextMode="Date"  ></asp:TextBox>
                        </div> 
                        <div class="col-md-2">
                            <asp:Label ID="LblEtiq11" runat="server" CssClass="control-label-2"  Text="Hora Inicia"></asp:Label>
                            <asp:TextBox ID="TxtHoraIni" runat="server"  CssClass="form-control" TextMode="Time" ></asp:TextBox>
                       </div> 
                        <div class="col-md-2">
                            <asp:Label ID="LblEtiq12" runat="server" CssClass="control-label-2"  Text="Hora Termina"></asp:Label>
                            <asp:TextBox ID="TxtHoraFin" runat="server"  CssClass="form-control"  TextMode="Time"   ></asp:TextBox>
                        </div> 
                    </div>     
                    <div class="row espacio">
                        <div class="col-lg-8">
                            <asp:Label id="LblEtiq7" runat="server" CssClass="control-label-2" text="Motivo" ></asp:Label>
                            <asp:TextBox ID="TxtPerMotivo" runat="server" CssClass="form-control" TextMode="MultiLine"  MaxLength="500" ></asp:TextBox>
                        </div>
                    </div> 

                    <div class="row espacio">
                        <div class="col-lg-2">
                            <asp:Button ID="BtnGuardar" runat="server" text="Guardar" CssClass="form-control btn-default" /> 
                        </div>
                        <div class="col-lg-2">
                            <asp:Button ID="BtnCancelar" runat="server" text="Cancelar" CssClass="form-control btn-default" /> 
                        </div>
                    </div>   
                </div>

                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:Label id="LblRegistro" runat="server" CssClass="control-label-2" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                    </div>
                </div>      
        
                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:GridView id="GvPermiso" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button">
                                    <ControlStyle CssClass="Form-control btn btn-default"></ControlStyle>
                                    <ItemStyle Height="10px" Width="10px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="PERMISO" HeaderText="Permiso">
                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TIPO_PERMISO" HeaderText="Tipo">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PERSONAL_CODIGO" HeaderText="Personal">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PERSONAL_NOMBRES" HeaderText="Nombres">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_INICIA" HeaderText="Inicia Permiso">
                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_TERMINA" HeaderText="Termina Permiso">
                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="HORA_INICIA" HeaderText="Hora Permiso">
                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="HORA_TERMINA" HeaderText="Hora fin permiso">
                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PERMISO_MOTIVO" HeaderText="Motivo">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True"></HeaderStyle>
                        </asp:GridView>
                    </div>
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GvPermiso" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnNuevo" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnCancelar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnGuardar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel> 
    </div>

</asp:Content>

