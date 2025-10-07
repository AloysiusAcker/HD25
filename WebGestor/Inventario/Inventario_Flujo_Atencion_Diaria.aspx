<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Flujo_Atencion_Diaria.aspx.vb" Inherits="Inventario_Inventario_Flujo_Atencion_Diaria" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:Label ID="Label1" runat="server" Text="Flujo de Atenciones" CssClass="Titulos"></asp:Label><br/><br/>
    <asp:Button ID="btnListar" runat="server" Text="Listar"  CssClass="btn btn-default"/>
    <asp:Button ID="BtnExportar" runat="server" Text="Exportar" CssClass="btn btn-default"/>
    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
        <ContentTemplate> 
	        <div class="form-group">
                <asp:Label ID="LblError" runat="server"  Font-Size="8pt" Font-Names="Arial" ForeColor="Red" CssClass="col-lg-2 control-label-2"></asp:Label>
 
            </div>

	        <div class="form-group">
                <asp:Label ID="Label3" runat="server" Text="Tipo de Guia" CssClass="col-lg-2 control-label-2" ></asp:Label>
                <div class="col-lg-3">    
                    <asp:DropDownList ID="DdlTipo" runat="server" CssClass="form-control">
                        <asp:ListItem Selected="True" Value="1">Guia de Remision</asp:ListItem>
                        <asp:ListItem Value="2">Guia Interna</asp:ListItem>
                        <asp:ListItem >&lt; Todos &gt;</asp:ListItem>
                    </asp:DropDownList>
                 </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="Fecha Inicio" CssClass="col-lg-2 control-label-2" ></asp:Label>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtFechaIni" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-lg-4">
                    <asp:ImageButton ID="btnI1" runat="server" FirstDayOfWeek="Wednesday" Height="30px" Width="50px" ImageUrl="~/Fotos/Calendario.bmp" ControlStyle-CssClass="btn btn-block" />
                    <cc1:CalendarExtender ID="CalendarExtender1" CssClass="custom-calendar" runat="server" TargetControlID="txtFechaIni" Format="dd/MM/yyyy" PopupButtonID="btnI1"></cc1:CalendarExtender>
                </div>
            </div>                        
                      
            
            <div class="form-group">
                <asp:Label ID="LblFechaIni" runat="server" CssClass="col-lg-2 control-label-2" Text="Fecha Fin"></asp:Label>
                <div class="col-lg-3">
                    <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-lg-4">
                    <asp:ImageButton ID="btnI2" runat="server" FirstDayOfWeek="Wednesday" Height="30px" Width="50px" ImageUrl="~/Fotos/Calendario.bmp" ControlStyle-CssClass="btn btn-block" />
                    <cc1:CalendarExtender ID="CalendarExtender2" CssClass="custom-calendar" runat="server" TargetControlID="txtFechaFin" Format="dd/MM/yyyy" PopupButtonID="btnI2"></cc1:CalendarExtender>
                </div>
            </div>

	        <div class="form-group">
                <asp:Label ID="LblRegistro" runat="server"  Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon" ></asp:Label>
            </div>

            <div class="row form-group col-md-10">
                <%--<div style="width: 700px; overflow: auto; height: 600px; border-right: gray 1px outset; border-top: gray 1px outset; border-left: gray 1px outset; border-bottom: gray 1px outset;" id="DIV1" runat="server">--%>
                <asp:GridView id="Flex" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                    <Columns>
                    <asp:BoundField DataField="COD_GUIA" HeaderText="Cod. Gu&#237;a">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Tipo_Guia" HeaderText="Tipo Guia">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Serie" HeaderText="Nro. Serie">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Correlativo" HeaderText="Numeracion">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="80px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Fecha" HeaderText="Fecha">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                        
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Familia" HeaderText="Familia">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></HeaderStyle>                        
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Destinatario" HeaderText="Destinatario">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></HeaderStyle>                        
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Remitente" HeaderText="Remitente">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></HeaderStyle>                        
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="TipoSolicitud" HeaderText="Tipo de Solicitud">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></HeaderStyle>                        
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Cod_Producto" HeaderText="Cod. Producto Banco">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></HeaderStyle>                        
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Cod_Producto_Sistema" HeaderText="Cod. Producto Sistema">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></HeaderStyle>                        
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion Producto Banco">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></HeaderStyle>                        
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="art_sistema" HeaderText="Descripcion Producto Sistema">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></HeaderStyle>                        
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Serie" HeaderText="Serie">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Placa" HeaderText="Placa">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Peso" HeaderText="Peso">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Peso_Total" HeaderText="Peso Total">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Volumen" HeaderText="Volumen">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Volumen_Total" HeaderText="Volumen Total">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="GUIA_ESTADO" HeaderText="Estado">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
                    </asp:BoundField>

                    </Columns>
                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                </asp:GridView>
            </div>
   <%--         </div>--%>
    
        </ContentTemplate>
        <Triggers>
            <asp1:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    
</asp:Content>

