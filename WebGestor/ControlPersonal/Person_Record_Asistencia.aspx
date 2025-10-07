<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Person_Record_Asistencia.aspx.vb" Inherits="Person_Record_Asistencia" title="GestorPlus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Record de Asistencia" CssClass="Titulos" />
            </div> 
        </div>
        
        <div class="row espacio">
            <div class="col-md-2">
                <asp:Label ID="lblFecha" runat="server" CssClass="control-label-2"  Text="Fecha"></asp:Label>
                <asp:TextBox ID="txtFechaIni" runat="server"  CssClass="form-control" ReadOnly="True"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="custom-calendar" TargetControlID="txtFechaIni" Format="dd/MM/yyyy" PopupButtonID="txtFechaIni" ></cc1:CalendarExtender>
            </div> 
            <div class="col-md-2">
                <asp:Label ID="lblHora" runat="server"  CssClass="control-label-2"  Text="Fecha Fin" ></asp:Label>
                <asp:TextBox id="txtFechaFin" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox> 
                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="custom-calendar" TargetControlID="txtFechaFin" Format="dd/MM/yyyy" PopupButtonID="txtFechaFin" ></cc1:CalendarExtender>
            </div> 
            <div class="col-md-2 col-xs-6">
                <asp:Label ID="Label1" runat="server" CssClass="control-label-2"  Text="Listar" ForeColor="white"></asp:Label>
                <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default"/>
            </div> 
            <div class="col-md-2 col-xs-6">
                <asp:Label ID="Label2" runat="server" CssClass="control-label-2"  Text="Exportar" ForeColor="white"></asp:Label>
                <asp:Button ID="BtnExportar" runat="server" Text="Exportar" ControlStyle-CssClass="form-control btn btn-default"/>
            </div>
        </div>      
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:Label id="lblRegistro" runat="server" CssClass="control-label-2" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                    </div>
                </div>      
        
                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:GridView id="FlexRecord" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField CommandName="Detalle" Text="Detalle" ButtonType="Button">
                                    <ControlStyle CssClass="Form-control btn btn-default"></ControlStyle>
                                    <ItemStyle Height="10px" Width="10px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="c0" HeaderText="#">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c1" HeaderText="C&#243;digo">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c2" HeaderText="Nombre del Personal">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c3" HeaderText="Cargo">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c4" HeaderText="Dias Trab.">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c5" HeaderText="Dias Tarde">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c6" HeaderText="Hrs. Trab.">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c7" HeaderText="Min. Tarde">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c8" HeaderText="Hrs. Ex. Servicio">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c9" HeaderText="Hrs. Extras">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True"></HeaderStyle>
                        </asp:GridView>
                    </div>
                </div>

                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:Label id="lblDetalle" runat="server" CssClass="control-label-2" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                    </div>
                </div>    
                
                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:GridView id="FlexDetalle" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:BoundField DataField="c0" HeaderText="#">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c1" HeaderText="Fecha">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c2" HeaderText="Tipo Registro">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c3" HeaderText="Hr. Trabajo Ent.">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c4" HeaderText="Hr. Trabajo Sal.">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c5" HeaderText="Hr. Refrigerio">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c6" HeaderText="Min. Tolerancia">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c7" HeaderText="Hr. Entrada">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c8" HeaderText="Hr. Salida">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c9" HeaderText="Min. Tarde">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c10" HeaderText="Hrs. Trabajadas">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c11" HeaderText="Hrs. Ext. Normales">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c12" HeaderText="Hrs. Ext. Servicio">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c13" HeaderText="N&#176; Servicio">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c14">
                                <ItemStyle Width="0px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c15" HeaderText="Motivo del Permiso">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c16" HeaderText="Latitud">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c17" HeaderText="Longitud">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"></HeaderStyle>
                        </asp:GridView>
                    </div>
                </div> 

                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" Visible ="false" >
                            <Columns>
                                <asp:BoundField DataField="c0" />
                                <asp:BoundField DataField="c1" />
                                <asp:BoundField DataField="c2" />
                                <asp:BoundField DataField="c3" />
                                <asp:BoundField DataField="c4" />
                                <asp:BoundField DataField="c5" />
                                <asp:BoundField DataField="c6" />
                                <asp:BoundField DataField="c7" />
                                <asp:BoundField DataField="c8" />
                                <asp:BoundField DataField="c9" />
                                <asp:BoundField DataField="c10" />
                                <asp:BoundField DataField="c11" />
                                <asp:BoundField DataField="c12" />
                                <asp:BoundField DataField="c13" />
                                <asp:BoundField DataField="c14" />
                                <asp:BoundField DataField="c15" />
                                <asp:BoundField DataField="c16" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="FlexRecord" EventName="RowCommand"/>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

