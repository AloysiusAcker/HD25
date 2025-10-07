<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Resumen_Oficinas.aspx.vb" Inherits="Inventario_Inventario_Resumen_Oficinas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <style>
            .rotatedHeaderCell {
                writing-mode: vertical-rl;
                transform: rotate(180deg);
                /*width: 30px; /* Ancho de la cabecera girada */*/
                text-align: match-parent; /* Alineación del texto */
                white-space: nowrap; /* Evitar que el texto se ajuste */
            }

    </style>

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Inventario - Resumen de Oficinas" CssClass="Titulos" />
            </div> 
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblInventario" runat="server" Text="Inventario :" CssClass="control-label-2" />
                <asp:DropDownList ID="DdlInventario" runat="server" CssClass="form-control" AutoPostBack="true">
                </asp:DropDownList>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3 col-xs-6">
                <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-md-3 col-xs-6">
                <asp:Button ID="BtnExportar" runat="server" Text="Exportar" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-md-3 col-xs-6">
                <asp:Button ID="BtnExportar2" runat="server" Text="Exportar Grilla" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
        </div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>                
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:Label ID="lblRegistro2" runat="server" class="control-label-2" Text="" visible="false" ></asp:Label>
                    </div> 
                </div>   
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:GridView ID="GvResumenCostos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                          <%--  <HeaderStyle CssClass="rotatedHeader" />--%>
                            <Columns>
                                <asp:BoundField DataField="numero_registro" HeaderText="#" SortExpression="numero_registro" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Oficina" SortExpression="Descripcion" />
                                <asp:BoundField DataField="ELEMEN_VALOR" HeaderText="Estado" SortExpression="ELEMEN_VALOR" />
                                <asp:BoundField DataField="DetalleID" HeaderText="" SortExpression="DetalleID" >
                                    <ItemStyle ForeColor="White" Width="0.1px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Encontrado en otro lugar" >
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell" >Encontrado en otro lugar</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Estado_3") %>
                                    </ItemTemplate>
                                    <ControlStyle Font-Bold="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Encontrado en otro lugar">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Encontrado en otro lugar por placar</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Estado_8") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inventariado OK">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Inventariado Ok por placar</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Estado_1") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inventariado OK">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Inventariado Ok por placar</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Estado_9") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nuevo Bien">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Nuevo Bien</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Estado_7") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Por placar (Encontrado por serie)">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Por placar (Encontrado por serie)</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Estado_5") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Blanco">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Blanco</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Estado_5") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total General">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Total General</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Total") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="personal programado">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">personal programado</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("personal_programado") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="personal que inventario">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">personal que inventario</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("personal_inventario") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha Programa">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Fecha Programa</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Fecha_Programa") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha Verificación">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Fecha Verificación</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("fecha_verificacion") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha Cierre de verificación">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Fecha Cierre de verificación</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("fecha_cierre") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hora Inicio">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Hora Inicio</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("hora_inicio") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hora Fin">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Hora Fin</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("hora_fin") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tiempo Inventario">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Tiempo Inventario</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("tiempo_inventario") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Costo de Verificación">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Costo de Verificación</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("COSTO_VERIFICACION") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="COSTO RECOJO DE LLAVES Y DEVOLUCION">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">COSTO RECOJO DE LLAVES Y DEVOLUCION</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("COSTO_RECOJO_LLAVES") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="COSTO MOVILIDAD">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">COSTO MOVILIDAD</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("COSTO_MOVILIDAD") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valor Inventario">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Valor Inventario</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("valor_inventario") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Costo Total">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Costo Total</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("costo_total") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valor Aprox. Utilidad">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Valor Aprox. Utilidad</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("valor_aprox_utilidad") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Costo Placado">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Costo Placado</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("COSTO_PLACADO") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Utilidad final">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Utilidad final</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("utilidad_final") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>  
            </ContentTemplate>
            <Triggers>                
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div> 

</asp:Content>

